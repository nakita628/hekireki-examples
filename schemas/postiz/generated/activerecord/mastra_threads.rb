class MastraThreads < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :resourceId, presence: true
  validates :title, presence: true
end
