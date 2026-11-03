class DelegationCredential < ApplicationRecord
  self.table_name = "DelegationCredential"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :enabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :serviceAccountKey, exclusion: { in: [ nil ] }
  validates :domain, presence: true, uniqueness: { scope: :organizationId }

  belongs_to :workspace_platform, foreign_key: "workspacePlatformId", inverse_of: :delegation_credentials
  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", inverse_of: :delegation_credentials
  has_many :selected_calendars, foreign_key: "delegationCredentialId", inverse_of: :delegation_credential,
                                dependent: :destroy
  has_many :destination_calendar, foreign_key: "delegationCredentialId", inverse_of: :delegation_credential,
                                  dependent: :destroy
  has_many :booking_references, foreign_key: "delegationCredentialId", inverse_of: :delegation_credential,
                                dependent: :nullify
  has_many :credentials, foreign_key: "delegationCredentialId", inverse_of: :delegation_credential, dependent: :destroy
end
