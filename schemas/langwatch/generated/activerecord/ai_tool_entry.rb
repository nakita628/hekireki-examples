class AiToolEntry < ApplicationRecord
  self.table_name = "AiToolEntry"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :order, default: 0
  attribute :enabled, default: true
  attribute :config, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :scope, presence: true
  validates :scopeId, presence: true
  validates :type, presence: true
  validates :displayName, presence: true
  validates :slug, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :ai_tool_entries
  has_many :teams, class_name: "AiToolEntryTeam", foreign_key: "entryId", inverse_of: :entry, dependent: :destroy
  has_many :departments, class_name: "AiToolEntryDepartment", foreign_key: "entryId", inverse_of: :entry,
                         dependent: :destroy
end
