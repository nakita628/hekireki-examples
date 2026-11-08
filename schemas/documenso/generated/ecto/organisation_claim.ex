defmodule Documenso.OrganisationClaim do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          original_subscription_claim_id: String.t() | nil,
          team_count: integer(),
          member_count: integer(),
          envelope_item_count: integer(),
          recipient_count: integer(),
          envelope_content_count: integer(),
          envelope_content_image_count: integer(),
          flags: map(),
          document_rate_limits: map(),
          document_quota: integer() | nil,
          email_rate_limits: map(),
          email_quota: integer() | nil,
          api_rate_limits: map(),
          api_quota: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_transport: Documenso.EmailTransport.t() | nil,
          organisation: Documenso.Organisation.t() | nil
        }

  schema "OrganisationClaim" do
    field(:id, :string, primary_key: true)
    field(:original_subscription_claim_id, :string, source: :originalSubscriptionClaimId)
    field(:team_count, :integer, source: :teamCount)
    field(:member_count, :integer, source: :memberCount)
    field(:envelope_item_count, :integer, source: :envelopeItemCount)
    field(:recipient_count, :integer, source: :recipientCount)
    field(:envelope_content_count, :integer, source: :envelopeContentCount)
    field(:envelope_content_image_count, :integer, source: :envelopeContentImageCount)
    field(:flags, :map)
    field(:document_rate_limits, :map, source: :documentRateLimits)
    field(:document_quota, :integer, source: :documentQuota)
    field(:email_rate_limits, :map, source: :emailRateLimits)
    field(:email_quota, :integer, source: :emailQuota)
    field(:api_rate_limits, :map, source: :apiRateLimits)
    field(:api_quota, :integer, source: :apiQuota)
    field(:email_transport_id, :string, source: :emailTransportId)
    belongs_to(:email_transport, Documenso.EmailTransport, foreign_key: :email_transport_id, define_field: false, type: :string)
    has_one(:organisation, Documenso.Organisation, foreign_key: :organisation_claim_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end