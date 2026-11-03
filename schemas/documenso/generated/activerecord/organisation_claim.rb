class OrganisationClaim < ApplicationRecord
  self.table_name = "OrganisationClaim"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :teamCount, presence: true
  validates :memberCount, presence: true
  validates :envelopeItemCount, presence: true
  validates :recipientCount, presence: true
  validates :flags, exclusion: { in: [ nil ] }
  validates :documentRateLimits, exclusion: { in: [ nil ] }
  validates :emailRateLimits, exclusion: { in: [ nil ] }
  validates :apiRateLimits, exclusion: { in: [ nil ] }

  belongs_to :email_transport, foreign_key: "emailTransportId", optional: true, inverse_of: :organisation_claims
  has_one :organisation, foreign_key: "organisationClaimId", inverse_of: :organisation_claim,
                         dependent: :restrict_with_error
end
