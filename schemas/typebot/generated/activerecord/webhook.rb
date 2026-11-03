class Webhook < ApplicationRecord
  self.table_name = "Webhook"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :method, presence: true
  validates :queryParams, exclusion: { in: [ nil ] }
  validates :headers, exclusion: { in: [ nil ] }

  belongs_to :typebot, foreign_key: "typebotId", inverse_of: :webhooks
end
