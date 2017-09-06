select * 
from Spellings s
left join SpellTests t on t.id = s.SpellTest_Id
where t.id is null;

select * 
from TestOccurances s
left join SpellTests t on t.id = s.SpellTestId
where t.id is null;

select * 
from TestAnswers s
left join SpellTests t on t.id = s.SpellTestId
where t.id is null;


--delete from TestAnswers where id in (31, 32,33,34);
--delete from spellings where id in (30)