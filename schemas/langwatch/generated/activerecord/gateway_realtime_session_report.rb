class GatewayRealtimeSessionReport < ApplicationRecord
  self.table_name = "GatewayRealtimeSessionReport"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :sessionId, presence: true, uniqueness: { scope: :reportKey }
  validates :reportKey, presence: true
  validates :projectId, presence: true
  validates :model, presence: true
  validates :usage, exclusion: { in: [ nil ] }
  validates :costNanoUsd, presence: true
end
