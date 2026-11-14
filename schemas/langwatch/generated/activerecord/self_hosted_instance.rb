class SelfHostedInstance < ApplicationRecord
  self.table_name = "SelfHostedInstance"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :userDomains, default: -> { [] }
  attribute :optionalMetricsReported, default: true
  attribute :hostnameReported, default: true
  attribute :reportCount, default: 0
  attribute :lastUnknownFields, default: 0
  attribute :raisedSignals, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :instanceId, presence: true, uniqueness: true
  validates :lastSeenAt, presence: true
end
