defmodule Documenso.Organisation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          name: String.t(),
          url: String.t(),
          customer_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organisation_claim: Documenso.OrganisationClaim.t() | nil,
          avatar_image: Documenso.AvatarImage.t() | nil,
          owner: Documenso.User.t() | nil,
          organisation_global_settings: Documenso.OrganisationGlobalSettings.t() | nil,
          organisation_authentication_portal: Documenso.OrganisationAuthenticationPortal.t() | nil,
          subscription: Documenso.Subscription.t() | nil,
          members: [Documenso.OrganisationMember.t()],
          invites: [Documenso.OrganisationMemberInvite.t()],
          groups: [Documenso.OrganisationGroup.t()],
          teams: [Documenso.Team.t()],
          email_domains: [Documenso.EmailDomain.t()],
          organisation_emails: [Documenso.OrganisationEmail.t()],
          monthly_stats: [Documenso.OrganisationMonthlyStat.t()],
          embed_signatures: [Documenso.EmbedSignature.t()]
        }

  schema "Organisation" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:PERSONAL, :ORGANISATION])
    field(:name, :string)
    field(:url, :string)
    field(:customer_id, :string, source: :customerId)
    field(:organisation_claim_id, :string, source: :organisationClaimId)
    field(:avatar_image_id, :string, source: :avatarImageId)
    field(:owner_user_id, :id, source: :ownerUserId)
    field(:organisation_global_settings_id, :string, source: :organisationGlobalSettingsId)
    field(:organisation_authentication_portal_id, :string, source: :organisationAuthenticationPortalId)
    belongs_to(:organisation_claim, Documenso.OrganisationClaim, foreign_key: :organisation_claim_id, define_field: false, type: :string)
    belongs_to(:avatar_image, Documenso.AvatarImage, foreign_key: :avatar_image_id, define_field: false, type: :string)
    belongs_to(:owner, Documenso.User, foreign_key: :owner_user_id, define_field: false)
    belongs_to(:organisation_global_settings, Documenso.OrganisationGlobalSettings, foreign_key: :organisation_global_settings_id, define_field: false, type: :string)
    belongs_to(:organisation_authentication_portal, Documenso.OrganisationAuthenticationPortal, foreign_key: :organisation_authentication_portal_id, define_field: false, type: :string)
    has_one(:subscription, Documenso.Subscription, foreign_key: :organisation_id)
    has_many(:members, Documenso.OrganisationMember, foreign_key: :organisation_id)
    has_many(:invites, Documenso.OrganisationMemberInvite, foreign_key: :organisation_id)
    has_many(:groups, Documenso.OrganisationGroup, foreign_key: :organisation_id)
    has_many(:teams, Documenso.Team, foreign_key: :organisation_id)
    has_many(:email_domains, Documenso.EmailDomain, foreign_key: :organisation_id)
    has_many(:organisation_emails, Documenso.OrganisationEmail, foreign_key: :organisation_id)
    has_many(:monthly_stats, Documenso.OrganisationMonthlyStat, foreign_key: :organisation_id)
    has_many(:embed_signatures, Documenso.EmbedSignature, foreign_key: :organisation_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end