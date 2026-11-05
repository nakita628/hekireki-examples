defmodule Chibisafe.Users do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          username: String.t(),
          password: String.t(),
          enabled: boolean(),
          storage_quota: String.t(),
          api_key: String.t() | nil,
          password_edited_at: DateTime.t() | nil,
          api_key_edited_at: DateTime.t() | nil,
          edited_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          files: [Chibisafe.Files.t()],
          links: [Chibisafe.ShortenedLinks.t()],
          roles: [Chibisafe.Roles.t()]
        }

  schema "users" do
    field(:uuid, :string)
    field(:username, :string)
    field(:password, :string)
    field(:enabled, :boolean, default: true)
    field(:storage_quota, :string, default: "0", source: :storageQuota)
    field(:api_key, :string, source: :apiKey)
    field(:password_edited_at, Chibisafe.PrismaDateTime, source: :passwordEditedAt)
    field(:api_key_edited_at, Chibisafe.PrismaDateTime, source: :apiKeyEditedAt)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    has_many(:files, Chibisafe.Files, foreign_key: :user_id)
    has_many(:links, Chibisafe.ShortenedLinks, foreign_key: :user_id)
    many_to_many(:roles, Chibisafe.Roles, join_through: "_rolesTousers", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end