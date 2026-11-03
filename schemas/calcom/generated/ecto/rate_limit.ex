defmodule Calcom.RateLimit do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          ttl: integer(),
          limit: integer(),
          block_duration: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          api_key: Calcom.ApiKey.t() | nil
        }

  schema "RateLimit" do
    field(:name, :string)
    field(:ttl, :integer)
    field(:limit, :integer)
    field(:block_duration, :integer, source: :blockDuration)
    field(:api_key_id, :string, source: :apiKeyId)
    belongs_to(:api_key, Calcom.ApiKey, foreign_key: :api_key_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end