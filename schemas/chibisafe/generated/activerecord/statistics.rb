class Statistics < ApplicationRecord
  self.inheritance_column = nil

  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt
end
