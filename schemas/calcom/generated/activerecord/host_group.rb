class HostGroup < ApplicationRecord
  self.table_name = "HostGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :host_groups
  has_many :hosts, foreign_key: "groupId", inverse_of: :group, dependent: :nullify
end
