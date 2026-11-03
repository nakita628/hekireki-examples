defmodule Postiz.Media do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          original_name: String.t() | nil,
          path: String.t(),
          deleted_at: DateTime.t() | nil,
          file_size: integer(),
          type: String.t(),
          thumbnail: String.t() | nil,
          alt: String.t() | nil,
          thumbnail_timestamp: integer() | nil,
          status: String.t(),
          processing_error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil,
          agencies: [Postiz.SocialMediaAgency.t()],
          user_picture: [Postiz.User.t()],
          oauth_apps: [Postiz.OAuthApp.t()]
        }

  schema "Media" do
    field(:name, :string)
    field(:original_name, :string, source: :originalName)
    field(:path, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:file_size, :integer, default: 0, source: :fileSize)
    field(:type, :string, default: "image")
    field(:thumbnail, :string)
    field(:alt, :string)
    field(:thumbnail_timestamp, :integer, source: :thumbnailTimestamp)
    field(:status, :string, default: "ready")
    field(:processing_error, :string, source: :processingError)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    has_many(:agencies, Postiz.SocialMediaAgency, foreign_key: :logo_id)
    has_many(:user_picture, Postiz.User, foreign_key: :picture_id)
    has_many(:oauth_apps, Postiz.OAuthApp, foreign_key: :picture_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end