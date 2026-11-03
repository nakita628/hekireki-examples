class MastraSkillBlobs < ApplicationRecord
  self.primary_key = "hash"

  alias_attribute :created_at, :createdAt

  validates :content, presence: true
  validates :size, presence: true
end
