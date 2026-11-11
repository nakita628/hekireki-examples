defmodule LiteLLM.LiteLLMTelemetryReport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          window_start: float(),
          window_end: float(),
          report: map(),
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_TelemetryReport" do
    field(:id, :string, primary_key: true)
    field(:window_start, :float)
    field(:window_end, :float)
    field(:report, :map)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end