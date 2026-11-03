class Credential < ApplicationRecord
  self.table_name = "Credential"
  self.inheritance_column = nil

  attribute :invalid, default: false

  validates :type, presence: true
  validates :key, exclusion: { in: [ nil ] }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :credentials
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :credentials
  belongs_to :app, foreign_key: "appId", optional: true, inverse_of: :credentials
  belongs_to :delegation_credential, foreign_key: "delegationCredentialId", optional: true, inverse_of: :credentials
  has_many :destination_calendars, foreign_key: "credentialId", inverse_of: :credential, dependent: :destroy
  has_many :selected_calendars, foreign_key: "credentialId", inverse_of: :credential, dependent: :destroy
  has_many :calendar_cache, class_name: "CalendarCache", foreign_key: "credentialId", inverse_of: :credential,
                            dependent: :destroy
  has_many :references, class_name: "BookingReference", foreign_key: "credentialId", inverse_of: :credential,
                        dependent: :nullify
  has_many :integration_attribute_syncs, foreign_key: "credentialId", inverse_of: :credential, dependent: :destroy
  has_many :host_locations, foreign_key: "credentialId", inverse_of: :credential, dependent: :nullify
end
