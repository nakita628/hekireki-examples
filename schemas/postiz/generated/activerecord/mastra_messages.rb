class MastraMessages < ApplicationRecord
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt

  validates :thread_id, presence: true
  validates :content, presence: true
  validates :role, presence: true
  validates :type, presence: true
end
