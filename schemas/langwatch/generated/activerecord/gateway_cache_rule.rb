class GatewayCacheRule < ApplicationRecord
  self.table_name = "GatewayCacheRule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :priority, default: 100
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :modeEnum, { respect: "RESPECT", force: "FORCE", disable: "DISABLE" }, validate: true

  validates :organizationId, presence: true
  validates :name, presence: true
  validates :matchers, exclusion: { in: [ nil ] }
  validates :action, exclusion: { in: [ nil ] }
  validates :createdById, presence: true
end
