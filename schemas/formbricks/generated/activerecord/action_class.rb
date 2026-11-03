class ActionClass < ApplicationRecord
  self.table_name = "ActionClass"
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :type, { code: "code", no_code: "noCode" }, validate: true

  validates :name, presence: true, uniqueness: { scope: :workspaceId }
  validates :key, uniqueness: { scope: :workspaceId, allow_nil: true }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :action_classes
  has_many :survey_triggers, foreign_key: "actionClassId", inverse_of: :action_class, dependent: :destroy
  has_many :creative_triggers, foreign_key: "actionClassId", inverse_of: :action_class, dependent: :destroy
end
