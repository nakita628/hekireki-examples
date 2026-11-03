class Prompt < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :type, default: "text"
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :tags, default: -> { [] }
  attribute :labels, default: -> { [] }

  validates :created_by, presence: true
  validates :prompt, exclusion: { in: [ nil ] }
  validates :name, presence: true, uniqueness: { scope: %i[project_id version] }
  validates :version, presence: true

  belongs_to :project, inverse_of: :prompt
  has_many :prompt_dependency, class_name: "PromptDependency", foreign_key: "parent_id", inverse_of: :parent,
                               dependent: :destroy
end
