defmodule AnythingLLM.CacheData do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          data: String.t(),
          belongs_to: String.t() | nil,
          by_id: integer() | nil,
          expires_at: DateTime.t() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "cache_data" do
    field(:name, :string)
    field(:data, :string)
    field(:belongs_to, :string, source: :belongsTo)
    field(:by_id, :integer, source: :byId)
    field(:expires_at, AnythingLLM.PrismaDateTime, source: :expiresAt)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end