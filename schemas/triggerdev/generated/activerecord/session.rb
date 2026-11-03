class Session < ApplicationRecord
  self.table_name = "Session"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isTest, default: false
  attribute :tags, default: -> { [] }
  attribute :currentRunVersion, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :environmentType, { production: "PRODUCTION", staging: "STAGING", development: "DEVELOPMENT", preview: "PREVIEW" },
       validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :type, presence: true
  validates :projectId, presence: true
  validates :runtimeEnvironmentId, presence: true,
                                   uniqueness: { scope: :externalId, conditions: -> { where.not(externalId: nil) } }
  validates :organizationId, presence: true
  validates :taskIdentifier, presence: true
  validates :triggerConfig, exclusion: { in: [ nil ] }

  has_many :runs, class_name: "SessionRun", foreign_key: "sessionId", inverse_of: :session, dependent: :destroy
end
