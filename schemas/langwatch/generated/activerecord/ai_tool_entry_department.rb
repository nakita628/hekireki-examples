class AiToolEntryDepartment < ApplicationRecord
  self.table_name = "AiToolEntryDepartment"

  attribute :id, default: -> { Nanoid.generate }

  validates :departmentId, presence: true, uniqueness: { scope: :entryId }

  belongs_to :entry, class_name: "AiToolEntry", foreign_key: "entryId", inverse_of: :departments
end
