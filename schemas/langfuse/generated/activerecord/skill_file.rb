class SkillFile < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :path, presence: true, uniqueness: { scope: %i[project_id skill_id] }

  belongs_to :project, inverse_of: :skill_file
  belongs_to :skill, inverse_of: :files
  belongs_to :blob, class_name: "SkillBlob", inverse_of: :files
end
