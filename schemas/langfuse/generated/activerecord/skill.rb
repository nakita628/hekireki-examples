class Skill < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :frontmatter, default: -> { JSON.parse("{}") }
  attribute :tags, default: -> { [] }
  attribute :labels, default: -> { [] }

  validates :created_by, presence: true
  validates :name, presence: true, uniqueness: { scope: %i[project_id version] }
  validates :description, presence: true
  validates :version, presence: true

  belongs_to :project, inverse_of: :skill
  has_many :files, class_name: "SkillFile", inverse_of: :skill, dependent: :destroy
end
