defmodule Postiz.SocialMediaAgency do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          website: String.t() | nil,
          slug: String.t() | nil,
          facebook: String.t() | nil,
          instagram: String.t() | nil,
          twitter: String.t() | nil,
          linked_in: String.t() | nil,
          youtube: String.t() | nil,
          tiktok: String.t() | nil,
          other_social_media: String.t() | nil,
          short_description: String.t(),
          description: String.t(),
          approved: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          logo: Postiz.Media.t() | nil,
          user: Postiz.User.t() | nil,
          niches: [Postiz.SocialMediaAgencyNiche.t()]
        }

  schema "SocialMediaAgency" do
    field(:name, :string)
    field(:website, :string)
    field(:slug, :string)
    field(:facebook, :string)
    field(:instagram, :string)
    field(:twitter, :string)
    field(:linked_in, :string, source: :linkedIn)
    field(:youtube, :string)
    field(:tiktok, :string)
    field(:other_social_media, :string, source: :otherSocialMedia)
    field(:short_description, :string, source: :shortDescription)
    field(:description, :string)
    field(:approved, :boolean, default: false)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:logo_id, :binary_id, source: :logoId)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:logo, Postiz.Media, foreign_key: :logo_id, define_field: false)
    belongs_to(:user, Postiz.User, foreign_key: :user_id, define_field: false)
    has_many(:niches, Postiz.SocialMediaAgencyNiche, foreign_key: :agency_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end