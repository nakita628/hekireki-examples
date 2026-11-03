class EvalTemplate < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :vars, default: -> { [] }

  enum :type, { llm_as_judge: "LLM_AS_JUDGE", code: "CODE", decision_model: "DECISION_MODEL", facet: "FACET" },
       default: :llm_as_judge, validate: true
  enum :source_code_language, { python: "PYTHON", typescript: "TYPESCRIPT" }, validate: { allow_nil: true }

  validates :name, presence: true,
                   uniqueness: { scope: %i[project_id version], conditions: -> { where.not(project_id: nil) } }
  validates :version, presence: true
  validates :source_code, length: { maximum: 262144 }

  belongs_to :project, optional: true, inverse_of: :eval_template
  has_many :job_configuration, class_name: "JobConfiguration", inverse_of: :eval_template, dependent: :nullify
end
