defmodule LiteLLM.LiteLLMProxyWorkerHeartbeat do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          worker_id: String.t(),
          hostname: String.t(),
          started_at: DateTime.t(),
          last_heartbeat_at: DateTime.t()
        }

  schema "LiteLLM_ProxyWorkerHeartbeat" do
    field(:worker_id, :string, primary_key: true)
    field(:hostname, :string)
    field(:started_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:last_heartbeat_at, LiteLLM.PrismaDateTime, autogenerate: true)
  end
end