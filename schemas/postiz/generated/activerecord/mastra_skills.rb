class MastraSkills < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :status, presence: true
end
