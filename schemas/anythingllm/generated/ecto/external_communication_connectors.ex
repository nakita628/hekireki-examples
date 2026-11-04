defmodule AnythingLLM.ExternalCommunicationConnectors do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          type: String.t(),
          config: String.t(),
          active: boolean(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "external_communication_connectors" do
    field(:type, :string)
    field(:config, :string, default: "{}")
    field(:active, :boolean, default: false)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end