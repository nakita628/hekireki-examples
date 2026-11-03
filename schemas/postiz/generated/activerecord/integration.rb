class Integration < ApplicationRecord
  self.table_name = "Integration"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :disabled, default: false
  attribute :inBetweenSteps, default: false
  attribute :refreshNeeded, default: false
  attribute :postingTimes, default: "[{\"time\":120}, {\"time\":400}, {\"time\":700}]"
  attribute :additionalSettings, default: "[]"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :internalId, presence: true, uniqueness: { scope: :organizationId }
  validates :name, presence: true
  validates :providerIdentifier, presence: true
  validates :type, presence: true
  validates :token, presence: true

  belongs_to :customer, foreign_key: "customerId", optional: true, inverse_of: :integrations
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :integration
  has_many :exising_plug_data, foreign_key: "integrationId", inverse_of: :integration, dependent: :restrict_with_error
  has_many :webhooks, class_name: "IntegrationsWebhooks", foreign_key: "integrationId", inverse_of: :integration,
                      dependent: :restrict_with_error
  has_many :order_items, foreign_key: "integrationId", inverse_of: :integration, dependent: :restrict_with_error
  has_many :plugs, foreign_key: "integrationId", inverse_of: :integration, dependent: :restrict_with_error
  has_many :posts, foreign_key: "integrationId", inverse_of: :integration, dependent: :restrict_with_error
end
