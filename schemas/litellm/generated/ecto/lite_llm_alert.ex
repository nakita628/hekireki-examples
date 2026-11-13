defmodule LiteLLM.LiteLLMAlert do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          detection_key: String.t(),
          type: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          status: String.t(),
          window_start: DateTime.t(),
          window_end: DateTime.t(),
          evidence: map(),
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_Alert" do
    field(:detection_key, :string)
    field(:type, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:status, :string, default: "open")
    field(:window_start, LiteLLM.PrismaDateTime)
    field(:window_end, LiteLLM.PrismaDateTime)
    field(:evidence, :map)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end