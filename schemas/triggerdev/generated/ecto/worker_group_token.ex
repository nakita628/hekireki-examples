defmodule Trigger.WorkerGroupToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token_hash: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          worker_group: Trigger.WorkerInstanceGroup.t() | nil
        }

  schema "WorkerGroupToken" do
    field(:id, :string, primary_key: true)
    field(:token_hash, :string, source: :tokenHash)
    has_one(:worker_group, Trigger.WorkerInstanceGroup, foreign_key: :token_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end