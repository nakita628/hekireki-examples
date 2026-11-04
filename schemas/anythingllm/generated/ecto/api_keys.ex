defmodule AnythingLLM.ApiKeys do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t() | nil,
          secret: String.t() | nil,
          created_by: integer() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "api_keys" do
    field(:name, :string)
    field(:secret, :string)
    field(:created_by, :integer, source: :createdBy)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end