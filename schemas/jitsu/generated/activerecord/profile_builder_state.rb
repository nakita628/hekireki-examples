class ProfileBuilderState < ApplicationRecord
  self.table_name = "ProfileBuilderState"
  self.primary_key = %w[profileBuilderId profileBuilderVersion totalInstances instanceIndex]

  alias_attribute :updated_at, :updatedAt

  validates :startedAt, presence: true
  validates :processedUsers, presence: true
  validates :totalUsers, presence: true
  validates :errorUsers, presence: true
  validates :speed, presence: true
end
