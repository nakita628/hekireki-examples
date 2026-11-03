class Prompt < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :type, default: "text"
  attribute :tags, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :friendly_id, presence: true, uniqueness: true
  validates :slug, presence: true, uniqueness: { scope: [ :projectId, :runtimeEnvironmentId ] }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :prompts
  belongs_to :project, foreign_key: "projectId", inverse_of: :prompts
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :prompts
  has_many :versions, class_name: "PromptVersion", foreign_key: "promptId", inverse_of: :prompt, dependent: :destroy
end
