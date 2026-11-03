class Agent < ApplicationRecord
  self.table_name = "Agent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :providerAgentId, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :agents
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :agents
  has_many :inbound_phone_numbers, class_name: "CalAiPhoneNumber", foreign_key: "inboundAgentId",
                                   inverse_of: :inbound_agent, dependent: :nullify
  has_many :outbound_phone_numbers, class_name: "CalAiPhoneNumber", foreign_key: "outboundAgentId",
                                    inverse_of: :outbound_agent, dependent: :nullify
end
