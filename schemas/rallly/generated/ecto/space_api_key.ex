defmodule Rallly.SpaceApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          prefix: String.t(),
          hashed_key: String.t(),
          last_used_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil
        }

  schema "space_api_keys" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:prefix, :string)
    field(:hashed_key, :string)
    field(:last_used_at, Rallly.PrismaDateTime)
    field(:expires_at, Rallly.PrismaDateTime)
    field(:revoked_at, Rallly.PrismaDateTime)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    timestamps(inserted_at_source: :created_at)
  end
end