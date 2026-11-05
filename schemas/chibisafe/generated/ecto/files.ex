defmodule Chibisafe.Files do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          name: String.t(),
          original: String.t(),
          type: String.t(),
          size: String.t(),
          hash: String.t(),
          ip: String.t(),
          source_url: String.t() | nil,
          is_s3: boolean(),
          is_watched: boolean(),
          edited_at: DateTime.t() | nil,
          quarantine: boolean(),
          inserted_at: DateTime.t(),
          user: Chibisafe.Users.t() | nil,
          quarantine_file: Chibisafe.QuarantineFiles.t() | nil,
          albums: [Chibisafe.Albums.t()],
          tags: [Chibisafe.Tags.t()]
        }

  schema "files" do
    field(:uuid, :string)
    field(:name, :string)
    field(:original, :string)
    field(:type, :string)
    field(:size, :string)
    field(:hash, :string)
    field(:ip, :string)
    field(:source_url, :string, source: :sourceUrl)
    field(:is_s3, :boolean, default: false, source: :isS3)
    field(:is_watched, :boolean, default: false, source: :isWatched)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    field(:quarantine, :boolean, default: false)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Chibisafe.Users, foreign_key: :user_id, define_field: false)
    has_one(:quarantine_file, Chibisafe.QuarantineFiles, foreign_key: :file_id, references: :uuid)
    many_to_many(:albums, Chibisafe.Albums, join_through: "_albumsTofiles", join_keys: [B: :id, A: :id])
    many_to_many(:tags, Chibisafe.Tags, join_through: "_filesTotags", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end