defmodule LiteLLM.LiteLLMCronJob do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          cronjob_id: String.t(),
          pod_id: String.t(),
          status: atom(),
          last_updated: DateTime.t(),
          ttl: DateTime.t()
        }

  schema "LiteLLM_CronJob" do
    field(:cronjob_id, :string, primary_key: true)
    field(:pod_id, :string)
    field(:status, Ecto.Enum, values: [:ACTIVE, :INACTIVE], default: :INACTIVE)
    field(:last_updated, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:ttl, LiteLLM.PrismaDateTime)
  end
end