class ShareWidget < ApplicationRecord
  attribute :public, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :id, presence: true, uniqueness: true
  validates :options, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :share_widgets
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :share_widget
end
