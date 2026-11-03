class UtmTemplate < ApplicationRecord
  self.table_name = "UtmTemplate"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :projectId, conditions: -> { where.not(projectId: nil) } }
  validates :utm_source, length: { maximum: 255 }
  validates :utm_medium, length: { maximum: 255 }
  validates :utm_campaign, length: { maximum: 255 }
  validates :utm_term, length: { maximum: 255 }
  validates :utm_content, length: { maximum: 255 }
  validates :ref, length: { maximum: 255 }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :utm_templates
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :utm_templates
end
