defmodule LiteLLM.LiteLLMLensTraceSignal do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          trace_id: String.t(),
          trace_ref: String.t(),
          config_key: String.t(),
          span_count: integer(),
          claimed_until: DateTime.t() | nil,
          classified_at: DateTime.t() | nil,
          data: map()
        }

  schema "LiteLLM_LensTraceSignal" do
    field(:trace_id, :string, primary_key: true)
    field(:trace_ref, :string, primary_key: true, default: "")
    field(:config_key, :string)
    field(:span_count, :integer)
    field(:claimed_until, LiteLLM.PrismaDateTime)
    field(:classified_at, LiteLLM.PrismaDateTime)
    field(:data, :map)
  end
end