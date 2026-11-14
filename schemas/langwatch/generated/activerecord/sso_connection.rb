class SsoConnection < ApplicationRecord
  self.table_name = "SsoConnection"
  self.inheritance_column = nil

  attribute :claimedDomains, default: -> { [] }
  attribute :domainClaims, default: -> { JSON.parse("[]") }
  attribute :approvedDomains, default: -> { [] }
  attribute :verifiedDomains, default: -> { [] }
  attribute :lapsedDomains, default: -> { [] }
  attribute :domainVerifications, default: -> { JSON.parse("[]") }
  attribute :arrivalPolicy, default: "refuse"
  attribute :allowsJit, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :migrationPhase, { setup: "SETUP", grace_legacy: "GRACE_LEGACY", grace_direct: "GRACE_DIRECT", finalizing: "FINALIZING", finalized: "FINALIZED" },
       validate: { allow_nil: true }

  validates :organizationId, presence: true
  validates :type, presence: true
  validates :state, presence: true
  validates :idpMetadata, exclusion: { in: [ nil ] }
  validates :source, presence: true
  validates :occurredAt, presence: true
  validates :lastEventId, presence: true
  validates :acceptedAt, presence: true
  validates :projectionVersion, presence: true

  has_one :reproof_cursor, class_name: "SsoConnectionReproofCursor", foreign_key: "connectionId",
                           inverse_of: :connection, dependent: :destroy
end
