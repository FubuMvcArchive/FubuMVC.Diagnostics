using System.Reflection;
using FubuMVC.Core.Http;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Model;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Diagnostics.Tests.Model
{
    [TestFixture]
    public class DiagnosticContextTester
    {
        [Test]
        public void current_context_and_group_with_normal_chain()
        {
            var currentChain = MockRepository.GenerateMock<ICurrentChain>();
            currentChain.Stub(x => x.OriginatingChain).Return(new BehaviorChain());

            var context = new DiagnosticContext(currentChain);

            context.CurrentChain().ShouldBeNull();
            context.CurrentGroup().ShouldBeNull();
        }

        [Test]
        public void current_context_on_a_diagnostic_chain()
        {
            var call = ActionCall.For<FakeFubuDiagnostics>(x => x.get_simple());
            var group = new DiagnosticGroup(Assembly.GetExecutingAssembly());
            var chain = new DiagnosticChain(group, call);

            var currentChain = MockRepository.GenerateMock<ICurrentChain>();
            currentChain.Stub(x => x.OriginatingChain).Return(chain);

            var context = new DiagnosticContext(currentChain);
            context.CurrentChain().ShouldBeTheSameAs(chain);
            context.CurrentGroup().ShouldBeTheSameAs(group);
        }


    }
}