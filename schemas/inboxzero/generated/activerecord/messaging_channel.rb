class MessagingChannel < ApplicationRecord
  self.table_name = "MessagingChannel"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isConnected, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :provider, { slack: "SLACK", teams: "TEAMS", telegram: "TELEGRAM" }, validate: true

  validates :id, uniqueness: { scope: :emailAccountId }
  validates :teamId, presence: true, uniqueness: { scope: [ :emailAccountId, :provider ] }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :messaging_channels
  has_many :routes, class_name: "MessagingRoute", foreign_key: "messagingChannelId", inverse_of: :messaging_channel,
                    dependent: :destroy
  has_many :automation_jobs, foreign_key: [ :messagingChannelId, :emailAccountId ],
                             primary_key: [ :id, :emailAccountId ], inverse_of: :messaging_channel, dependent: :destroy
  has_many :actions, foreign_key: [ :messagingChannelId, :messagingChannelEmailAccountId ],
                     primary_key: [ :id, :emailAccountId ], inverse_of: :messaging_channel, dependent: :nullify
  has_many :executed_actions, foreign_key: "messagingChannelId", inverse_of: :messaging_channel, dependent: :nullify
end
