using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMS
{
	public abstract class DataFile
	{
		#region Fields

		private XDocument file;
		private readonly Uri fileUrl;
		private readonly TimeSpan updateInterval;
		#endregion

		#region Properties

		protected Uri FileUrl
		{
			get { return this.fileUrl; }
		}

		protected DateTime? LastUpdate { get; set; }

		protected TimeSpan UpdateInterval
		{
			get { return this.updateInterval; }
		}
		#endregion

		#region Ctor

		internal DataFile(Uri fileUrl, TimeSpan updateInterval)
		{
			if (fileUrl == null)
			{
				throw new ArgumentNullException("fileUrl");
			}

			if (updateInterval < TimeSpan.Zero)
			{
				throw new ArgumentException("updateInterval");
			}

			this.fileUrl = fileUrl;
			this.updateInterval = updateInterval;
		}
		#endregion

		#region Methods

		protected async Task<XDocument> GetFile()
		{
			if ((this.file == null) ||
				!this.LastUpdate.HasValue ||
				(DateTime.Now - this.LastUpdate.Value > this.UpdateInterval))
			{
				this.file = await LoadFile();
			}

			return this.file;
		}

		private async Task<XDocument> LoadFile()
		{
			throw new NotImplementedException();
		}
		#endregion
	}

	public abstract class DataFile<T> : DataFile
	{
		#region Ctor

		internal DataFile(Uri fileUrl, TimeSpan updateInterval)
			: base(fileUrl, updateInterval) { } 
		#endregion

		#region Methods

		public async Task<T> GetData()
		{
			XDocument fileData = await GetFile();
			T data = await ParseData(fileData);

			return data;
		}

		protected abstract Task<T> ParseData(XDocument fileData); 
		#endregion
	}
}
