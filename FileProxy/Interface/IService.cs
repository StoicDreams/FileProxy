﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoicDreams.FileProxy.Interface
{
	public interface IService
	{
		Task<(bool IsMatched, FileData fileData)> HandleProxyIfMatchedAsync(string requestPath);
	}
}
