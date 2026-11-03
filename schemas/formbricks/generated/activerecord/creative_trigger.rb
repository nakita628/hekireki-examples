class CreativeTrigger < ApplicationRecord
  self.table_name = "CreativeTrigger"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :creativeId, uniqueness: { scope: :actionClassId }

  belongs_to :creative, foreign_key: "creativeId", inverse_of: :triggers
  belongs_to :action_class, foreign_key: "actionClassId", inverse_of: :creative_triggers
end
