class SelfHostedNotice < ApplicationRecord
  self.table_name = "SelfHostedNotice"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :severity, presence: true
  validates :title, presence: true
  validates :body, presence: true
  validates :status, presence: true
end
