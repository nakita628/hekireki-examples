class CustomEvent < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :projectId }
  validates :items, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :custom_events
end
