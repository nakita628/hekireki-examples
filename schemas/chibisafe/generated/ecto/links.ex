defmodule Chibisafe.Links do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          user_id: integer(),
          album_id: integer(),
          identifier: String.t(),
          views: integer(),
          enabled: boolean(),
          enable_download: boolean(),
          expires_at: DateTime.t() | nil,
          edited_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "links" do
    field(:uuid, :string)
    field(:user_id, :integer, source: :userId)
    field(:album_id, :integer, source: :albumId)
    field(:identifier, :string)
    field(:views, :integer, default: 0)
    field(:enabled, :boolean, default: true)
    field(:enable_download, :boolean, default: false, source: :enableDownload)
    field(:expires_at, Chibisafe.PrismaDateTime, source: :expiresAt)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end