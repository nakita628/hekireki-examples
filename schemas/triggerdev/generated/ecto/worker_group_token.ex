defmodule Trigger.WorkerGroupToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token_hash: String.t(),
          previous_token_hash: String.t() | nil,
          previous_token_valid_until: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          worker_group: Trigger.WorkerInstanceGroup.t() | nil
        }

  schema "WorkerGroupToken" do
    field(:id, :string, primary_key: true)
    field(:token_hash, :string, source: :tokenHash)
    field(:previous_token_hash, :string, source: :previousTokenHash)
    field(:previous_token_valid_until, Trigger.PrismaDateTime, source: :previousTokenValidUntil)
    has_one(:worker_group, Trigger.WorkerInstanceGroup, foreign_key: :token_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end