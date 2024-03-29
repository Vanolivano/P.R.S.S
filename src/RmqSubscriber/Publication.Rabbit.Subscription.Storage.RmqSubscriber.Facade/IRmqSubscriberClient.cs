﻿using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade
{
	public interface IRmqSubscriberClient
	{
		ISuccessData SendData(IPersonArgs personArgs);
	}
}