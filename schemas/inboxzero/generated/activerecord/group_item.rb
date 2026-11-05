class GroupItem < ApplicationRecord
  self.table_name = "GroupItem"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :exclude, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { from: "FROM", subject: "SUBJECT", body: "BODY" }, prefix: true, validate: true
  enum :source, { ai: "AI", user: "USER", label_removed: "LABEL_REMOVED", label_added: "LABEL_ADDED" },
       validate: { allow_nil: true }

  validates :value, presence: true,
                    uniqueness: { scope: [ :groupId, :type ], conditions: -> { where.not(groupId: nil) } }

  belongs_to :group, foreign_key: "groupId", optional: true, inverse_of: :items
end
