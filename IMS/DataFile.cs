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
		#endregion

		#region Properties

		protected Uri FileUrl { get; }

		protected DateTime? LastUpdate { get; set; }

		protected TimeSpan UpdateInterval { get; }
		#endregion

		#region Ctor

		internal DataFile(Uri fileUrl, TimeSpan updateInterval)
		{
			if (fileUrl == null)
			{
				throw new ArgumentNullException(nameof(fileUrl));
			}

			if (updateInterval < TimeSpan.Zero)
			{
				throw new ArgumentException(nameof(updateInterval));
			}

			this.FileUrl = fileUrl;
			this.UpdateInterval = updateInterval;
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
