class Webhook < ApplicationRecord
  self.table_name = "Webhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :pId, presence: true, uniqueness: true
  validates :name, presence: true
  validates :url, presence: true
  validates :secret, presence: true
  validates :triggers, exclusion: { in: [ nil ] }

  belongs_to :team, foreign_key: "teamId", inverse_of: :webhooks
end
