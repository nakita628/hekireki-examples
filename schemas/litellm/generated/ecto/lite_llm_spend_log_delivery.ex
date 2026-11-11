defmodule LiteLLM.LiteLLMSpendLogDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          delivery_id: String.t(),
          request_id: String.t(),
          state: atom(),
          accounting_payload: map(),
          is_batch: boolean(),
          attempts: integer(),
          next_attempt_at: DateTime.t(),
          applied_at: DateTime.t() | nil,
          last_error: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_SpendLogDelivery" do
    field(:delivery_id, :string, primary_key: true, read_after_writes: true)
    field(:request_id, :string)
    field(:state, Ecto.Enum, values: [:pending, :applied, :parked], default: :pending)
    field(:accounting_payload, :map)
    field(:is_batch, :boolean)
    field(:attempts, :integer, default: 0)
    field(:next_attempt_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:applied_at, LiteLLM.PrismaDateTime)
    field(:last_error, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end